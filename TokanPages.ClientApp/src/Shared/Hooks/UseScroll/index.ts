import * as React from "react";

interface UseScrollResultProps {
    isScrollingUp: boolean;
    isScrolledTop: boolean;
}

export const useScroll = (): UseScrollResultProps => {
    let lastScrollTop = window.scrollY || document.documentElement.scrollTop;

    const [isScrollingUp, setIsScrollingUp] = React.useState(window.scrollY === 0);
    const [isScrolledTop, setIsScrolledTop] = React.useState(window.scrollY === 0);

    const handleScrolling = React.useCallback(() => {
        const scrollTopPosition = window.scrollY || document.documentElement.scrollTop;

        if (scrollTopPosition > lastScrollTop) {
            setIsScrollingUp(false);
        } else {
            setIsScrollingUp(true);
        }

        setIsScrolledTop(window.scrollY === 0);
        return lastScrollTop = scrollTopPosition <= 0 ? 0 : scrollTopPosition;

    }, [window.scrollY, document.documentElement.scrollTop]);

    React.useEffect(() => {
        window.addEventListener("scroll", handleScrolling);
        return () => window.removeEventListener("scroll", handleScrolling);
    }, []);
    
    return { 
        isScrollingUp: isScrollingUp,
        isScrolledTop: isScrolledTop,
    };
}
