import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { UserNotesView } from "./View/userNotesView";
import { UserNoteResultDto } from "../../../../Api/Models";
import { ApplicationState } from "../../../../Store/Configuration";
import { UserNotesReadAction, UserNoteCreateAction, UserNoteUpdateAction } from "../../../../Store/Actions";
import { RECEIVED_ERROR_MESSAGE } from "../../../../Shared/constants";
import { ReactChangeEvent } from "../../../../Shared/types";
import { OperationStatus } from "../../../../Shared/enums";

export interface UserNotesProps {
    background?: React.CSSProperties;
}

export const UserNotes = (props: UserNotesProps): React.ReactElement => {
    const dispatch = useDispatch();

    const error = useSelector((state: ApplicationState) => state.applicationError);
    const userNotes = useSelector((state: ApplicationState) => state.userNotesRead);
    const userNoteCreate = useSelector((state: ApplicationState) => state.userNoteCreate);
    const userNoteUpdate = useSelector((state: ApplicationState) => state.userNoteUpdate);
    const contentPageData = useSelector((state: ApplicationState) => state.contentPageData);
    const content = contentPageData?.components?.accountUserNotes;
    const hasError = error?.errorMessage === RECEIVED_ERROR_MESSAGE;

    const hasUserNoteCreateNotStarted = userNoteCreate.status === OperationStatus.notStarted;
    const hasUserNoteCreateFinished = userNoteCreate.status === OperationStatus.hasFinished;
    const hasUserNoteUpdateNotStarted = userNoteUpdate.status === OperationStatus.notStarted;
    const hasUserNoteUpdateFinished = userNoteUpdate.status === OperationStatus.hasFinished;

    const [form, setForm] = React.useState({ note: "" });
    const [selection, setSelection] = React.useState<UserNoteResultDto | undefined>(undefined);
    const [hasProgress, setProgress] = React.useState(false);

    const selectionHandler = React.useCallback((index: number) => {
        const data = userNotes.response.notes[index];
        setSelection(data);
        setForm({ note: data.note });
    }, [selection, userNotes]);

    const formHandler = React.useCallback((event: ReactChangeEvent) => {
        setForm({ ...form, [event.currentTarget.name]: event.currentTarget.value });
    }, [form]);

    const saveButtonHandler = React.useCallback(() => {
        if (!hasProgress) {
            setProgress(true);
        }
    }, [hasProgress]);

    const removeButtonHandler = React.useCallback(() => {

    }, []);

    /* ADD NEW USER NOTE */
    React.useEffect(() => {
        if (selection) {
            return;
        }

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
    }, [selection, hasError, hasProgress, hasUserNoteCreateNotStarted, hasUserNoteCreateFinished]);

    /* UPDATE SELECTED USER NOTE */
    React.useEffect(() => {
        if (!selection) {
            return;
        }

        if (hasError) {
            setProgress(false);
            dispatch(UserNoteUpdateAction.clear());
            return;
        }

        if (hasUserNoteUpdateNotStarted && hasProgress) {
            dispatch(UserNoteUpdateAction.update({ id: selection.id,  note: form.note }));
        }

        if (hasUserNoteUpdateFinished) {
            setProgress(false);
            dispatch(UserNoteUpdateAction.clear());
        }
    }, [selection, hasError, hasProgress, hasUserNoteUpdateNotStarted, hasUserNoteUpdateFinished]);

    React.useEffect(() => {
        dispatch(UserNotesReadAction.get({ noCache: true }));
    }, []);

    return(
        <UserNotesView 
            isLoading={contentPageData?.isLoading}
            hasProgress={hasProgress}
            userNotes={userNotes.response.notes}
            captionText={content?.caption}
            descriptionText={content?.description}
            listLabel={content?.listLabel}
            noteLabel={content?.noteLabel}
            onRowClick={(index: number) => selectionHandler(index)}
            selection={selection}
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
