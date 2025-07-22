import * as React from "react";
import { useLocation } from "react-router-dom";
import { Properties } from "../Types";

export const ClearPageStart = (props: Properties): React.ReactElement => {
    const location = useLocation();

    React.useEffect(() => {
        window.scrollTo({ top: 0, left: 0, behavior: "smooth" });
    }, [location]);

    return <>{props.children}</>;
};
