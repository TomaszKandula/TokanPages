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

    const [isScrollingUp, setIsScrollingUp] = React.useState(window.scrollY === 0);
    const [isScrolledTop, setIsScrolledTop] = React.useState(window.scrollY === 0);

    const handleScrolling = React.useCallback(() => {
        const referenceValue = props.offset ?? 0;
        const scrollTopPosition = window.scrollY || document.documentElement.scrollTop;

        if (scrollTopPosition > lastScrollTop) {
            setIsScrollingUp(false);
        } else {
            setIsScrollingUp(true);
        }

        setIsScrolledTop(window.scrollY <= referenceValue);
        lastScrollTop = scrollTopPosition <= referenceValue ? referenceValue : scrollTopPosition;

        return lastScrollTop;
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
