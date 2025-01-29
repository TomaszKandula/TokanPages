import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { UserNotesView } from "./View/userNotesView";
import { UserNoteResultDto } from "../../../../Api/Models";
import { ApplicationState } from "../../../../Store/Configuration";
import { UserNotesReadAction, UserNoteCreateAction, UserNoteUpdateAction, UserNoteDeleteAction } from "../../../../Store/Actions";
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
    const userNoteDelete = useSelector((state: ApplicationState) => state.userNoteDelete);
    const contentPageData = useSelector((state: ApplicationState) => state.contentPageData);
    const content = contentPageData?.components?.accountUserNotes;
    const hasError = error?.errorMessage === RECEIVED_ERROR_MESSAGE;

    const hasUserNoteCreateNotStarted = userNoteCreate.status === OperationStatus.notStarted;
    const hasUserNoteCreateFinished = userNoteCreate.status === OperationStatus.hasFinished;
    const hasUserNoteUpdateNotStarted = userNoteUpdate.status === OperationStatus.notStarted;
    const hasUserNoteUpdateFinished = userNoteUpdate.status === OperationStatus.hasFinished;
    const hasUserNoteDeleteNotStarted = userNoteDelete.status === OperationStatus.notStarted;
    const hasUserNoteDeleteFinished = userNoteDelete.status === OperationStatus.hasFinished;

    const [form, setForm] = React.useState({ note: "" });
    const [selection, setSelection] = React.useState<UserNoteResultDto | undefined>(undefined);
    const [hasProgress, setProgress] = React.useState(false);
    const [canDelete, setDelete] = React.useState(false);
    const [canRefresh, setRefresh] = React.useState(false);

    const selectionHandler = React.useCallback((index: number) => {
        const data = userNotes.response.notes[index];
        setSelection(data);
        setForm({ note: data.note });
    }, [selection, userNotes]);

    const formHandler = React.useCallback((event: ReactChangeEvent) => {
        setForm({ ...form, [event.currentTarget.name]: event.currentTarget.value });
    }, [form]);

    const clearButtonHandler = React.useCallback(() => {
        setSelection(undefined);
        setForm({ note: "" });
    }, [selection, form]);

    const saveButtonHandler = React.useCallback(() => {
        if (!hasProgress) {
            setProgress(true);
        }
    }, [hasProgress]);

    const removeButtonHandler = React.useCallback(() => {
        if (!selection) {
            return;
        }

        if (!canDelete) {
            setDelete(true);
            setProgress(true);
        }
    }, [selection, canDelete, hasProgress]);

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
            setRefresh(true);
            setProgress(false);
            setSelection({ 
                note: form.note,
                id: userNoteCreate.response.id,
                createdAt: userNoteCreate.response.createdAt,
                createdBy: userNoteCreate.response.createdBy,
            });

            dispatch(UserNoteCreateAction.clear());
        }

    }, [selection, hasError, hasProgress, userNoteCreate, hasUserNoteCreateNotStarted, hasUserNoteCreateFinished]);

    /* UPDATE SELECTED USER NOTE */
    React.useEffect(() => {
        if (!selection) {
            return;
        }

        if (canDelete) {
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
            setRefresh(true);
            dispatch(UserNoteUpdateAction.clear());
        }
    }, [selection, canDelete, hasError, hasProgress, hasUserNoteUpdateNotStarted, hasUserNoteUpdateFinished]);

    /* REMOVE SELECTED USER NOTE */
    React.useEffect(() => {
        if (!selection) {
            return;
        }

        if (!canDelete) {
            return;
        }

        if (hasError) {
            setProgress(false);
            setDelete(false);
            dispatch(UserNoteDeleteAction.clear());
            return;
        }

        if (hasUserNoteDeleteNotStarted && hasProgress) {
            dispatch(UserNoteDeleteAction.delete({ id: selection.id }));
        }

        if (hasUserNoteDeleteFinished) {
            setProgress(false);
            setDelete(false);
            setRefresh(true);
            setSelection(undefined);
            setForm({ note: "" });
            dispatch(UserNoteDeleteAction.clear());
        }
    }, [selection, canDelete, hasError, hasProgress, hasUserNoteDeleteNotStarted, hasUserNoteDeleteFinished]);

    /* ON REFRESH EVENT */
    React.useEffect(() => {
        if (canRefresh) {
            dispatch(UserNotesReadAction.get({ noCache: true }));
            setRefresh(false);
        }
    }, [canRefresh]);

    /* GET USER NOTES */
    React.useEffect(() => {
        setTimeout(() => dispatch(UserNotesReadAction.get({ noCache: false })), 1500);
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
            clearButtonText={content?.buttons?.clearText}
            clearButtonHandler={clearButtonHandler}
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
