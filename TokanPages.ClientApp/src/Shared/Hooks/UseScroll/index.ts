import * as React from "react";

interface UseScrollResultProps {
    isScrollingUp: boolean;
    isScrolledTop: boolean;
}

interface UseScrollProps {
    offset?: number;
}

export const useScroll = (props: UseScrollProps): UseScrollResultProps => {
    let lastScrollTop = window.scrollY || document.documentElement.scrollTop;
    const isScrolledTop = window.scrollY === 0;
    const offset = props.offset ?? 0;

    const [isScrollingUp, setIsScrollingUp] = React.useState(isScrolledTop);

    const handleScrolling = React.useCallback(() => {
        const scrollTopPosition = window.scrollY || document.documentElement.scrollTop;

        if (scrollTopPosition > lastScrollTop - offset) {
            setIsScrollingUp(false);
        } else {
            setIsScrollingUp(true);
        }

        lastScrollTop = scrollTopPosition <= offset ? offset : scrollTopPosition;
    }, [window.scrollY, document.documentElement.scrollTop]);

    React.useEffect(() => {
        window.addEventListener("scroll", handleScrolling);
        return () => window.removeEventListener("scroll", handleScrolling);
    }, []);

    return {
        isScrollingUp: isScrollingUp,
        isScrolledTop: isScrolledTop,
    };
};
