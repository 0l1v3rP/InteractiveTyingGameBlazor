function scrollToBottom() {
    let scrollableDiv = document.getElementById('chatMessages');
    scrollableDiv.scrollTop = scrollableDiv
        .scrollHeight;
}