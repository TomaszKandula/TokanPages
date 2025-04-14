import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { ContentPageDataAction } from "../../../Store/Actions";
import { ApplicationState } from "../../../Store/Configuration";

export const usePageContent = (components: string[], pageId: string): void => {
    const dispatch = useDispatch();
    const state = useSelector((state: ApplicationState) => state);
    const language = state.applicationLanguage;

    React.useEffect(() => {
        dispatch(ContentPageDataAction.request(components, pageId));
    }, [language?.id]);
}
