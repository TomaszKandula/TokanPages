import * as React from "react";

export interface UseDimensionsResult {
    width: number;
    height: number;
    isMobile: boolean;
    isTablet: boolean;
    isDesktop: boolean;
}

export const useDimensions = (): UseDimensionsResult => {
    const [width, setWidth] = React.useState(window.innerWidth);
    const [height, setHeight] = React.useState(window.innerHeight);
    const [isMobile, setIsMobile] = React.useState(false);
    const [isTablet, setIsTablet] = React.useState(false);
    const [isDesktop, setIsDesktop] = React.useState(false);

    const updateDimensions = (): void => {
        setWidth(window.innerWidth);
        setHeight(window.innerHeight);
    };

    React.useEffect(() => {
        /* MOBILE */
        if (width <= 768) {
            setIsMobile(true);
            setIsTablet(false);
            setIsDesktop(false);
        }

        /* TABLET */
        if (width > 768 && width < 1024) {
            setIsMobile(false);
            setIsTablet(true);
            setIsDesktop(false);
        }

        /* DESKTOP */
        if (width >= 1024) {
            setIsMobile(false);
            setIsTablet(false);
            setIsDesktop(true);
        }
    }, [width]);

    React.useEffect(() => {
        window.addEventListener("resize", updateDimensions);
        return () => window.removeEventListener("resize", updateDimensions);
    }, []);

    return {
        width,
        height,
        isMobile,
        isTablet,
        isDesktop,
    };
};
