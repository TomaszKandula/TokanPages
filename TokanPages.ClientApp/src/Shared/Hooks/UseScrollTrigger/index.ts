import * as React from "react";

let lastScrollTop = window.scrollY || document.documentElement.scrollTop;

export const useScrollTrigger = (): boolean => {
    const [isScrollTop, setIsScrollTop] = React.useState(window.scrollY === 0);

    const handleScrolling = (): number => {
        const scrollTopPosition = window.scrollY || document.documentElement.scrollTop;

        if (scrollTopPosition > lastScrollTop) {
            setIsScrollTop(false);
        } else {
            setIsScrollTop(true);
        }

        return lastScrollTop = scrollTopPosition <= 0 ? 0 : scrollTopPosition;
    }

    React.useEffect(() => {
        window.addEventListener("scroll", handleScrolling);
        return () => window.removeEventListener("scroll", handleScrolling);
    }, []);

    return isScrollTop;
}
