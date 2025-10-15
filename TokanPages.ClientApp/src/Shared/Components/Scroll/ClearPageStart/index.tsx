import * as React from "react";
import { useLocation } from "react-router-dom";
import { ClearPageStartProps } from "../Types";

export const ClearPageStart = (props: ClearPageStartProps): React.ReactElement => {
    const location = useLocation();

    React.useEffect(() => {
        window.scrollTo({ top: 0, left: 0, behavior: "smooth" });
    }, [location]);

    return <>{props.children}</>;
};
