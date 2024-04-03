function disposeInterop(dotNetRef) {
    window.addEventListener('beforeunload', async () => {
        await dotNetRef.invokeMethodAsync('DisposeComponent');
    });
}