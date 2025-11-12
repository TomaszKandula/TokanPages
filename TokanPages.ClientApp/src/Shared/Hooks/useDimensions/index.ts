import * as React from "react";

export interface UseDimensionsResult {
    width: number;
    height: number;
    hasPortrait: boolean | null;
    hasLandscape: boolean | null;
    isMobile: boolean | null;
    isTablet: boolean | null;
    isDesktop: boolean | null;
}

export const useDimensions = (): UseDimensionsResult => {
    const [width, setWidth] = React.useState(window.innerWidth);
    const [height, setHeight] = React.useState(window.innerHeight);
    const [hasLandscape, setHasLandscape] = React.useState<boolean | null>(null);
    const [hasPortrait, setHasPortrait] = React.useState<boolean | null>(null);
    const [isMobile, setIsMobile] = React.useState<boolean | null>(null);
    const [isTablet, setIsTablet] = React.useState<boolean | null>(null);
    const [isDesktop, setIsDesktop] = React.useState<boolean | null>(null);

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
