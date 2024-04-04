function initVideoPlayer(videoId, dotNetRef) {
    const videoElement = document.getElementById(videoId);
    const player = videojs(videoElement, {
        controls: false, 
        autoplay: false, 
        bigPlayButton: false, 
        fluid: false,
    });
    const rightInput = document.getElementById('rightTypingInput');

    document.getElementById('textInputWrapper').addEventListener('click', () => {
        setCursorToStart();
    })
    player.on('click',async (event) => {
        if (!player.currentSrc() || player.currentSrc() === "") {
            event.preventDefault();
            event.stopPropagation();
            await dotNetRef.invokeMethodAsync('OpenConfiguration');
        }
    });

    rightInput.addEventListener('keydown', (event) => {
        if (event.key === 'Backspace' || ['ArrowUp', 'ArrowDown', 'ArrowLeft', 'ArrowRight'].includes(event.key)) {
            setCursorToStart();
            event.preventDefault();
        }
    }); 

    rightInput.addEventListener('click', (event) => {
        setCursorToStart();
        event.preventDefault();
    });
    
    rightInput.addEventListener('keypress', (event) => { event.preventDefault() })

    let gameStatus = {
        ended: false,
        started: false,
        startTime: null,
        correctCharacters: 0,
        allCharacters: 0
    };

    let keyPressFun = { fun: null };
    
    let wasTriggered;

    const onLoad = async (duration) => {
        if (!wasTriggered) {
            wasTriggered = true;
            let maxStart = player.duration() - (duration * 60);
            let seed = await dotNetRef.invokeMethodAsync('GetSeedValue');
            let randomStart = Math.floor(seed * (maxStart));
            player.currentTime(randomStart);
            await new Promise(resolve => setTimeout(resolve, 3000));
            await dotNetRef.invokeMethodAsync('PlayerReady');
            let isReady = false;
            do {
                isReady = await dotNetRef.invokeMethodAsync('MatchReady');
                if (isReady) {
                    await dotNetRef.invokeMethodAsync('UpdateSubtitles', randomStart);
                    await dotNetRef.invokeMethodAsync('SpinnerVisibility', false);
                    await new Promise(resolve => setTimeout(resolve, 1000));
                    gameStatus.ended = false;
                    player.on('timeupdate', async () => await handleTimeUpdate(dotNetRef, player, gameStatus));
                    player.play();
                    gameStatus.started = true;
                    let subtitle = await dotNetRef.invokeMethodAsync('GetCurrentSubtitle');
                    setSubtitle(subtitle);
                    gameStatus.startTime = Date.now();
                    await dotNetRef.invokeMethodAsync('StartGame');
                    gameStatus.correctCharacters = 0;
                    gameStatus.allCharacters = 0;
                    break;
                } else {
                    await new Promise(resolve => setTimeout(resolve, 500));
                }
            } while(!isReady)
        }
    };

    window.updateVideoSource = async (videoPath, duration) => {
        await dotNetRef.invokeMethodAsync('SpinnerVisibility', true);
        wasTriggered = false;
        player.src({
            type: 'video/youtube',
            src: videoPath,
            techOrder: ['youtube'],
            youtube: {
                customVars: {
                    wmode: 'transparent',
                    controls: 0,
                },
            },
        });
        player.on("loadedmetadata", async () => await onLoad(duration));
        configueTextInput(dotNetRef, gameStatus, keyPressFun);
        player.load();
    };

    window.endGame = () => {
        gameStatus.ended = true;
        player.reset();
        rightInput.removeEventListener('keypress', keyPressFun.fun);
        rightInput.value = '';
        document.getElementById('leftTypingDisplay').innerHTML = '';
    }
}
function setCursorToStart() {
    let input = document.getElementById('rightTypingInput');
    input.setSelectionRange(0, 0);
    input.focus();
}

async function handleTimeUpdate(dotNetRef, player, gameStatus) {
    const result = await dotNetRef.invokeMethodAsync('UpdateSubtitles', player.currentTime());

    if (result) {
        const subtitle = await dotNetRef.invokeMethodAsync('GetCurrentSubtitle');
        player.pause(); 

        const checkInputValue = () => {
            if (!gameStatus.ended) {
                if (document.getElementById('rightTypingInput').value !== '') {
                    setTimeout(checkInputValue, 100);
                } else {
                    player.play();
                    setSubtitle(subtitle)
                    if (document.getElementById('leftTypingDisplay').lastElementChild) {
                        document.getElementById('leftTypingDisplay').lastElementChild.style.marginRight = '8px';
                    }
                }
            }
        };
        checkInputValue();
    }
}

function configueTextInput(dotNetRef, gameStatus, keyPressFun) {
    const rightInput = document.getElementById('rightTypingInput');
    const leftInput = document.getElementById('leftTypingDisplay');
    function calculateCPM() {
        const elapsedTime = (Date.now() - gameStatus.startTime) / 1000;
        return parseInt(gameStatus.correctCharacters / ((elapsedTime > 60 ? elapsedTime : 60) / 60));
    }

    function calculateAccuracy() {
        return parseFloat(((100 / gameStatus.allCharacters) * gameStatus.correctCharacters).toFixed(2));
    }

    function setResultChar(isCorrect) {
        const color = isCorrect ? 'green' : 'red';
        const char = rightInput.value.charAt(0);
        if (char === ' ') {
            leftInput.lastElementChild.style.marginRight = '8px';
        } else {

            leftInput.innerHTML += `<span style="color:${color}">${char}</span>`;
        }
        
        rightInput.value = rightInput.value.substring(1);
        setCursorToStart();
        handleOverflow();
    }

    function handleOverflow() {
        let totalWidth = 0;
        for (let child of leftInput.childNodes) {
            totalWidth += child.offsetWidth;
        }

        while (leftInput.scrollWidth > leftInput.clientWidth && leftInput.childNodes.length > 0) {
            leftInput.removeChild(leftInput.firstChild);
        }
    }

    keyPressFun.fun = async (event) => {
        if (gameStatus.started) {
            let correctChar = event.key === rightInput.value.charAt(0);
            if (event.key === rightInput.value.charAt(0)) {
                ++gameStatus.correctCharacters;
                await dotNetRef.invokeMethodAsync('SetCPM', calculateCPM())
            }
            setResultChar(correctChar)
            await dotNetRef.invokeMethodAsync('CharPressed', event.key, correctChar);
            ++gameStatus.allCharacters;
            await dotNetRef.invokeMethodAsync('SetAccuracy', (calculateAccuracy()));
        }
        else {
            event.preventDefault();
        }
    }
    rightInput.addEventListener('keypress', keyPressFun.fun);
}
function setSubtitle(subtitle) {
    const typedText = normalizeSubtitle(subtitle);
    document.getElementById('rightTypingInput').value = typedText;
    setCursorToStart();
}

function normalizeSubtitle(subtitle) {
    return subtitle.text
        .replace(/…/g, '...')
        .replace(/‒/g, '-')
        .replace(/„/g, '"')
        .replace(/“/g, '"')
        .replace(/\n/g, ' ')
}