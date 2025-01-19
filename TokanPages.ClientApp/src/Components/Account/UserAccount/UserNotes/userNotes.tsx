import * as React from "react";
import { UserNotesView } from "./View/userNotesView";

export const UserNotes = (): React.ReactElement => {

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
        />
    );
}
