import * as React from "react";
import { useSelector } from "react-redux";
import { Link } from "react-router-dom";
import { ApplicationState } from "Store/Configuration";

interface RedirectToProps {
    path: string;
    name: string;
}

export const RedirectTo = (props: RedirectToProps): React.ReactElement => {
    const languageId = useSelector((state: ApplicationState) => state.applicationLanguage?.id);
    return (
        <Link to={`/${languageId}${props.path}`} rel="noopener nofollow">
            {props.name}
        </Link>
    );
};
