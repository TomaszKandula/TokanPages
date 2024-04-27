export const IsRunningOnIOS = (): boolean => {
    return /iPad|iPhone|iPod/.test(navigator.userAgent);
};
