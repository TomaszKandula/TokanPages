import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { UserNotesView } from "./View/userNotesView";
import { ApplicationState } from "../../../../Store/Configuration";
import { UserNotesAction } from "../../../../Store/Actions/Users/userNotes";
import { RECEIVED_ERROR_MESSAGE } from "../../../../Shared/constants";

export interface UserNotesProps {
    background?: React.CSSProperties;
}

export const UserNotes = (props: UserNotesProps): React.ReactElement => {
    const dispatch = useDispatch();

    const error = useSelector((state: ApplicationState) => state.applicationError);
    const userNotes = useSelector((state: ApplicationState) => state.userNotes);
    const contentPageData = useSelector((state: ApplicationState) => state.contentPageData);
    const template = contentPageData?.components?.templates;
    const content = contentPageData?.components?.accountUserNotes;

    const hasError = error?.errorMessage === RECEIVED_ERROR_MESSAGE;

    console.log(template);
    console.log(dispatch);
    console.log(contentPageData);
    console.log(hasError);

    React.useEffect(() => {
        dispatch(UserNotesAction.get({ noCache: true }));
    }, []);

    return(
        <UserNotesView 
            isLoading={false}
            hasProgress={false}
            userNotes={userNotes.response.notes}
            captionText={content?.caption}
            descriptionText={content?.header}
            onRowClick={(id: string) => { console.log(id) }}
            removeButtonText={content?.buttons?.removeText}
            removeButtonHandler={() => { console.log("removeButtonHandler") }}
            saveButtonText={content?.buttons?.saveText}
            saveButtonHandler={() => { console.log("saveButtonHandler") }}
            messageValue=""
            messageHandler={() => { console.log("messageHandler") }}
            messageMultiline={true}
            background={props.background}
        />
    );
}
