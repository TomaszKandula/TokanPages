import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { UserNotesView } from "./View/userNotesView";
import { ApplicationState } from "../../../../Store/Configuration";
import { RECEIVED_ERROR_MESSAGE } from "Shared/constants";

export interface UserNotesProps {
    background?: React.CSSProperties;
}

export const UserNotes = (props: UserNotesProps): React.ReactElement => {
    const dispatch = useDispatch();

    const error = useSelector((state: ApplicationState) => state.applicationError);
    const contentPageData = useSelector((state: ApplicationState) => state.contentPageData);

    
    const hasError = error?.errorMessage === RECEIVED_ERROR_MESSAGE;

    console.log(props);
    console.log(dispatch);
    console.log(contentPageData);
    console.log(hasError);

    return(
        <UserNotesView 
            isLoading={false}
            hasProgress={false}
            userNotes={[{ id: "abcd", note: "note 1" }, { id: "efgh", note: "note 2" }]}
            captionText="User Notes"
            descriptionText="Some text"
            onRowClick={(id: string) => { console.log(id) }}
            removeButtonText="Remove"
            removeButtonHandler={() => { console.log("removeButtonHandler") }}
            saveButtonText="Save"
            saveButtonHandler={() => { console.log("saveButtonHandler") }}
            messageValue=""
            messageHandler={() => { console.log("messageHandler") }}
            messageMultiline={true}
        />
    );
}
