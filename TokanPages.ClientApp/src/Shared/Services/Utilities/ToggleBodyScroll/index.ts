export const ToggleBodyScroll = (isBodyScrollEnabled: boolean): void => {
    const html = document.getElementsByTagName("html")[0];
    if (isBodyScrollEnabled) {
        window.document.body.style.removeProperty("overflow");
        html.classList.remove("is-lock-scroll");
    } else {
        window.document.body.style.overflow = "hidden";
        html.classList.add("is-lock-scroll")
    }
};
