import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { UserNotesView } from "./View/userNotesView";
import { ApplicationState } from "../../../../Store/Configuration";
import { UserNotesAction, UserNoteCreateAction } from "../../../../Store/Actions";
import { RECEIVED_ERROR_MESSAGE } from "../../../../Shared/constants";
import { ReactChangeEvent } from "../../../../Shared/types";
import { OperationStatus } from "../../../../Shared/enums";

export interface UserNotesProps {
    background?: React.CSSProperties;
}

export const UserNotes = (props: UserNotesProps): React.ReactElement => {
    const dispatch = useDispatch();

    const error = useSelector((state: ApplicationState) => state.applicationError);
    const userNotes = useSelector((state: ApplicationState) => state.userNotes);
    const userNoteCreate = useSelector((state: ApplicationState) => state.userNoteCreate);
    const contentPageData = useSelector((state: ApplicationState) => state.contentPageData);
    const template = contentPageData?.components?.templates;
    const content = contentPageData?.components?.accountUserNotes;

    const hasUserNoteCreateNotStarted = userNoteCreate.status === OperationStatus.notStarted;
    const hasUserNoteCreateFinished = userNoteCreate.status === OperationStatus.hasFinished;
    const hasError = error?.errorMessage === RECEIVED_ERROR_MESSAGE;

    const [form, setForm] = React.useState({ note: "" });
    const [selection, setSelection] = React.useState("");
    const [hasProgress, setProgress] = React.useState(false);

    const formHandler = React.useCallback(
        (event: ReactChangeEvent) => {
            setForm({ ...form, [event.currentTarget.name]: event.currentTarget.value });
        },
        [form]
    );

    const saveButtonHandler = React.useCallback(() => {
        if (!hasProgress) {
            setProgress(true);
        }
    }, [hasProgress]);

    const removeButtonHandler = React.useCallback(() => {

    }, []);

    React.useEffect(() => {
        if (hasError) {
            setProgress(false);
            dispatch(UserNoteCreateAction.clear());
            return;
        }

        if (hasUserNoteCreateNotStarted && hasProgress) {
            dispatch(UserNoteCreateAction.add({ note: form.note }));
        }

        if (hasUserNoteCreateFinished) {
            setProgress(false);
            dispatch(UserNoteCreateAction.clear());
        }
    }, [hasError, hasProgress, hasUserNoteCreateNotStarted, hasUserNoteCreateFinished]);

    React.useEffect(() => {
        dispatch(UserNotesAction.get({ noCache: true }));
    }, []);

    console.log(template);
    console.log(selection);

    return(
        <UserNotesView 
            isLoading={false}
            hasProgress={hasProgress}
            userNotes={userNotes.response.notes}
            captionText={content?.caption}
            descriptionText={content?.description}
            listLabel={content?.listLabel}
            noteLabel={content?.noteLabel}
            onRowClick={(id: string) => { setSelection(id) }}
            removeButtonText={content?.buttons?.removeText}
            removeButtonHandler={removeButtonHandler}
            saveButtonText={content?.buttons?.saveText}
            saveButtonHandler={saveButtonHandler}
            messageForm={form}
            messageHandler={formHandler}
            messageMultiline={true}
            background={props.background}
        />
    );
}
