export const ToggleBodyScroll = (isBodyScrollEnabled: boolean): void => {
    if (isBodyScrollEnabled) {
        window.document.body.style.removeProperty("overflow");
    } else {
        window.document.body.style.overflow = "hidden";
    }
};
