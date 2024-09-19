import * as React from "react";

export const useHash = (): string => {
    const [hash, setHash] = React.useState(window.location.hash);

    React.useEffect(() => {
        const onHashChange = (): void => {
            setHash(window.location.hash);
        };

        window.addEventListener("hashchange", onHashChange);
        return () => window.removeEventListener("hashchange", onHashChange);
    }, []);

    return hash;
};
