function initVideoPlayer(videoId, dotNetRef) {
    const videoElement = document.getElementById(videoId);
    const player = videojs(videoElement, {
        controls: false, 
        autoplay: false, 
        bigPlayButton: false, 
        fluid: false,
    });
    const input = document.getElementById('typingInput');

    player.on('click',async (event) => {
        if (!player.currentSrc() || player.currentSrc() === "") {
            event.preventDefault();
            event.stopPropagation();
            await dotNetRef.invokeMethodAsync('OpenConfiguration');
        }
    });

    input.addEventListener('keydown', (event) => {
        if (event.key === 'Backspace' || ['ArrowUp', 'ArrowDown', 'ArrowLeft', 'ArrowRight'].includes(event.key)) {
            setCursorToStart();
            event.preventDefault();
        }
    });

    window.addEventListener('beforeunload', async () => {
        await dotNetRef.invokeMethodAsync('DisposeComponent');
    });

    input.addEventListener('click', (event) => {
        setCursorToStart();
        event.preventDefault();
    });
    
    input.addEventListener('keypress', (event) => { event.preventDefault() })

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
        input.removeEventListener('keypress', keyPressFun.fun);
        input.value = '';
    }
}
function setCursorToStart() {
    document.getElementById('typingInput').setSelectionRange(0, 0);
}

async function handleTimeUpdate(dotNetRef, player, gameStatus) {
    const result = await dotNetRef.invokeMethodAsync('UpdateSubtitles', player.currentTime());

    if (result) {
        const subtitle = await dotNetRef.invokeMethodAsync('GetCurrentSubtitle');
        player.pause(); 

        const checkInputValue = () => {
            if (!gameStatus.ended) {
                if (document.getElementById('typingInput').value !== '') {
                    setTimeout(checkInputValue, 100);
                } else {
                    setSubtitle(subtitle)
                    player.play();
                }
            }
        };
        checkInputValue();
    }
}

function configueTextInput(dotNetRef, gameStatus, keyPressFun) {
    const input = document.getElementById('typingInput');
    function calculateCPM() {
        const elapsedTime = (Date.now() - gameStatus.startTime) / 1000;
        return parseInt(gameStatus.correctCharacters / ((elapsedTime > 60 ? elapsedTime : 60) / 60));
    }

    function calculateAccuracy() {
        return parseFloat(((100 / gameStatus.allCharacters) * gameStatus.correctCharacters).toFixed(2));
    }
 
    keyPressFun.fun = async (event) => {
        if (gameStatus.started) {
            if (event.key === input.value.charAt(0)) {
                input.value = input.value.substring(1);
                setCursorToStart();
                ++gameStatus.correctCharacters;
                await dotNetRef.invokeMethodAsync('SetCPM', calculateCPM())
            }
            else {
                await dotNetRef.invokeMethodAsync('MissedChar', event.key);
            }
            ++gameStatus.allCharacters;
            await dotNetRef.invokeMethodAsync('SetAccuracy', (calculateAccuracy()));
        }
        else {
            event.preventDefault();
        }
    }
    input.addEventListener('keypress', keyPressFun.fun);
}
function setSubtitle(subtitle) {
    const typedText = normalizeSubtitle(subtitle);
    document.getElementById('typingInput').value = typedText;
}
function normalizeSubtitle(subtitle) {
    return subtitle.text
        .replace(/…/g, '...')
        .replace(/‒/g, '-')
}

window.addEventListener('beforeunload', async () => {
    await DotNet.invokeMethodAsync('InteractiveTyingGameBlazor', 'Dispose');
});