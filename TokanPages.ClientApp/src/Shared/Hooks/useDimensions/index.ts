import * as React from "react";

interface UseDimensionsResult {
    width: number;
    height: number;
}

export const useDimensions = (): UseDimensionsResult => {
    const [width, setWidth]   = React.useState(window.innerWidth);
    const [height, setHeight] = React.useState(window.innerHeight);

    const updateDimensions = (): void => {
        setWidth(window.innerWidth);
        setHeight(window.innerHeight);
    }

    React.useEffect(() => {
        window.addEventListener("resize", updateDimensions);
        return () => window.removeEventListener("resize", updateDimensions);
    }, []);

    return {
        width,
        height,
    }
}
