import * as React from "react";

export interface UseDimensionsResult {
    width: number;
    height: number;
    hasPortrait: boolean;
    hasLandscape: boolean;
    isMobile: boolean;
    isTablet: boolean;
    isDesktop: boolean;
}

export const useDimensions = (): UseDimensionsResult => {
    const [width, setWidth] = React.useState(window.innerWidth);
    const [height, setHeight] = React.useState(window.innerHeight);
    const [hasLandscape, setHasLandscape] = React.useState(false);
    const [hasPortrait, setHasPortrait] = React.useState(false);
    const [isMobile, setIsMobile] = React.useState(false);
    const [isTablet, setIsTablet] = React.useState(false);
    const [isDesktop, setIsDesktop] = React.useState(false);

    const updateDimensions = (): void => {
        setWidth(window.innerWidth);
        setHeight(window.innerHeight);
    };

    React.useEffect(() => {
        if (width > height) {
            setHasPortrait(false);
            setHasLandscape(true);
        } else {
            setHasPortrait(true);
            setHasLandscape(false);
        }
    }, [width, height]);

    React.useEffect(() => {
        let widthValue;
        if (hasLandscape && width < 1024) {
            widthValue = height;
        } else {
            widthValue = width;
        }

        /* MOBILE */
        if (widthValue <= 768) {
            setIsMobile(true);
            setIsTablet(false);
            setIsDesktop(false);
        }

        /* TABLET */
        if (widthValue > 768 && widthValue < 1024) {
            setIsMobile(false);
            setIsTablet(true);
            setIsDesktop(false);
        }

        /* DESKTOP */
        if (widthValue >= 1024) {
            setIsMobile(false);
            setIsTablet(false);
            setIsDesktop(true);
        }
    }, [width, height, hasLandscape]);

    React.useEffect(() => {
        window.addEventListener("resize", updateDimensions);
        return () => window.removeEventListener("resize", updateDimensions);
    }, []);

    return {
        width,
        height,
        hasPortrait,
        hasLandscape,
        isMobile,
        isTablet,
        isDesktop,
    };
};
