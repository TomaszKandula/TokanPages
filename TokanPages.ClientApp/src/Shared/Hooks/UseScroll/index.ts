import * as React from "react";

interface UseScrollResultProps {
    isScrollingUp: boolean;
    isScrolledTop: boolean;
}

interface UseScrollProps {
    treshold?: number;
}

const THRESHOLD = 0.5;

export const useScroll = (props: UseScrollProps): UseScrollResultProps => {
    let lastScrollTop = window.scrollY || document.documentElement.scrollTop;
    const treshold = props.treshold ?? THRESHOLD;

    const [isScrollingUp, setIsScrollingUp] = React.useState(window.scrollY < treshold);
    const [isScrolledTop, setIsScrolledTop] = React.useState(window.scrollY < treshold);

    const handleScrolling = React.useCallback(() => {
        const scrollTopPosition = window.scrollY || document.documentElement.scrollTop;

        if (scrollTopPosition > lastScrollTop) {
            setIsScrollingUp(false);
        } else {
            setIsScrollingUp(true);
        }

        lastScrollTop = scrollTopPosition < treshold ? treshold : scrollTopPosition;
        setIsScrolledTop(window.scrollY < treshold);
    }, [window.scrollY, document.documentElement.scrollTop]);

    React.useEffect(() => {
        if (window.scrollY < treshold) {
            setIsScrolledTop(true);
        } else {
            setIsScrolledTop(false);
        }
    }, [window.scrollY]);

    React.useEffect(() => {
        window.addEventListener("scroll", handleScrolling);
        return () => window.removeEventListener("scroll", handleScrolling);
    }, []);

    return {
        isScrollingUp: isScrollingUp,
        isScrolledTop: isScrolledTop,
    };
};
