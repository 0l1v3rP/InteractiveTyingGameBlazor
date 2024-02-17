function initVideoPlayer(videoId, dotNetRef) {
    const video = document.getElementById(videoId);
    video.addEventListener('timeupdate', () =>  handleTimeUpdate(dotNetRef, video));
    video.addEventListener('ended', () => handleVideoEnd(dotNetRef));
}

 function handleTimeUpdate(dotNetRef, video) {
    dotNetRef.invokeMethodAsync('updateSubtitles', video.currentTime)
    return;
}

function handleVideoEnd(dotNetRef) {
    
}

var input = document.getElementById('typingInput');
function setCursorToStart() {
    input.setSelectionRange(0, 0);
}

input.addEventListener('keypress', function (event) {
    if (event.key === input.value.charAt(0)) {
        input.value = input.value.substring(1);
        setCursorToStart();
    }
    event.preventDefault();
});

input.addEventListener('keydown', function (event) {
    if (event.key === 'Backspace' || ['ArrowUp', 'ArrowDown', 'ArrowLeft', 'ArrowRight'].includes(event.key)) {
        setCursorToStart();
        event.preventDefault();
    }
});

input.addEventListener('click', function (event) {
    setCursorToStart();
    event.preventDefault();
});

function setSubtitle(subtitle) {
    const typedText = subtitle.lines.join(' ').replace(/…/g, '...');
    document.getElementById('typingInput').value = typedText;
}