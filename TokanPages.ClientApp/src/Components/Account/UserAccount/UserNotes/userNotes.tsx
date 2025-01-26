import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { UserNotesView } from "./View/userNotesView";
import { ApplicationState } from "../../../../Store/Configuration";
import { UserNotesAction } from "../../../../Store/Actions/Users/userNotes";
import { RECEIVED_ERROR_MESSAGE } from "../../../../Shared/constants";
import { ReactChangeEvent, ReactKeyboardEvent } from "../../../../Shared/types";

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

    const [form, setForm] = React.useState({ note: "" });
    const [selectedNote, setSelectedNote] = React.useState("");

    const keyHandler = React.useCallback(
        (event: ReactKeyboardEvent) => {
            if (event.code === "Enter") {
                saveButtonHandler();
            }
        },
        [form.note]
    );
    
    const formHandler = React.useCallback(
        (event: ReactChangeEvent) => {
            setForm({ ...form, [event.currentTarget.name]: event.currentTarget.value });
        },
        [form]
    );

    const saveButtonHandler = React.useCallback(() => {

    }, []);

    const removeButtonHandler = React.useCallback(() => {

    }, []);

    React.useEffect(() => {
        dispatch(UserNotesAction.get({ noCache: true }));
    }, []);

    console.log(template);
    console.log(dispatch);
    console.log(contentPageData);
    console.log(hasError);
    console.log(selectedNote);

    return(
        <UserNotesView 
            isLoading={false}
            hasProgress={false}
            userNotes={userNotes.response.notes}
            captionText={content?.caption}
            descriptionText={content?.header}
            onRowClick={(id: string) => { setSelectedNote(id) }}
            removeButtonText={content?.buttons?.removeText}
            removeButtonHandler={removeButtonHandler}
            saveButtonText={content?.buttons?.saveText}
            saveButtonHandler={saveButtonHandler}
            keyHandler={keyHandler}
            messageForm={form}
            messageHandler={formHandler}
            messageMultiline={true}
            background={props.background}
        />
    );
}
