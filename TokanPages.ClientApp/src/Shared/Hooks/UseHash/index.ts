import * as React from "react";

export const useHash = () => {
    const [hash, setHash] = React.useState(window.location.hash);

    React.useEffect(() => {
        const onHashChange = () => {
            setHash(window.location.hash);
        };

        window.addEventListener("hashchange", onHashChange);
        return () => window.removeEventListener("hashchange", onHashChange);
    }, []);

    return hash;
};
