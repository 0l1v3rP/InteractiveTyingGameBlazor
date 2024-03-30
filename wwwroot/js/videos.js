function previewIsAccessible(accessible) {
    if(accessible) 
        document.getElementById('Preview').classList.remove('e-overlay')
    else 
        document.getElementById('Preview').classList.add('e-overlay') 
}