import * as React from "react";
import AOS from "aos";
import "aos/dist/aos.css";

const InitializeAnimations = (): NodeJS.Timer => {
    AOS.init({ once: false, disable: false });
    return setInterval(() => AOS.refresh(), 900);
};

export const useAnimation = (): void => {
    React.useEffect(() => {
        const intervalId = InitializeAnimations();
        return () => clearInterval(intervalId);
    }, []);
}
