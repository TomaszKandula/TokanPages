import * as React from "react";

export const useScrollTrigger = (): boolean => {
    const [isScrollTop, setIsScrollTop] = React.useState(window.scrollY === 0);

    let lastScrollTop = window.scrollY || document.documentElement.scrollTop;

    const handleScrolling = React.useCallback(() => {
        const scrollTopPosition = window.scrollY || document.documentElement.scrollTop;

        if (scrollTopPosition > lastScrollTop) {
            setIsScrollTop(false);
        } else {
            setIsScrollTop(true);
        }

        return lastScrollTop = scrollTopPosition <= 0 ? 0 : scrollTopPosition;
    }, [lastScrollTop]);

    React.useEffect(() => {
        window.addEventListener("scroll", handleScrolling);
        return () => window.removeEventListener("scroll", handleScrolling);
    }, []);

    return isScrollTop;
}
