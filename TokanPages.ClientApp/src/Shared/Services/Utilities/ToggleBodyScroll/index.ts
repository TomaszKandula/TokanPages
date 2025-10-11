export const ToggleBodyScroll = (isBodyScrollEnabled: boolean): void => {
    const html = document.getElementsByTagName("html")[0];
    if (isBodyScrollEnabled) {
        html.classList.remove("is-lock-scroll");
    } else {
        html.classList.add("is-lock-scroll")
    }
};
