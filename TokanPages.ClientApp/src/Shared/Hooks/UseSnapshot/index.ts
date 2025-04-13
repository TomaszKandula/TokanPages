import * as React from "react";
import { useSelector } from "react-redux";
import { ApplicationState } from "../../../Store/Configuration";
import { TryPostStateSnapshot } from "../../../Shared/Services/SpaCaching";

export const useSnapshot = () => {
    const state = useSelector((state: ApplicationState) => state);
    const language = state.applicationLanguage;

    React.useEffect(() => {
        if (language?.id !== "") {
            TryPostStateSnapshot(state);
        }
    }, [state]);
}
