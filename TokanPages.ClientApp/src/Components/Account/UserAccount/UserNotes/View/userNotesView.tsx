import * as React from "react";
import { Skeleton } from "@material-ui/lab";
import { UserNoteResultDto } from "../../../../../Api/Models";
import { CustomDivider } from "../../../../../Shared/Components";
import { ReactChangeEvent } from "../../../../../Shared/types";
import Validate from "validate.js";
import {
    Backdrop,
    Button,
    Card,
    CardContent,
    CircularProgress,
    Container,
    ListItem,
    ListItemText,
    TextField,
    Typography,
} from "@material-ui/core";
import "./userNotesView.css";

interface UserNotesViewProps {
    isLoading: boolean;
    hasProgress: boolean;
    userNotes?: UserNoteProps[] | undefined;
    captionText: string;
    descriptionText: string;
    listLabel: string;
    noteLabel: string;
    onRowClick: (index: number) => void;
    selection?: UserNoteResultDto | undefined;
    clearButtonText: string;
    clearButtonHandler: () => void;
    removeButtonText: string;
    removeButtonHandler: () => void;
    saveButtonText: string;
    saveButtonHandler: () => void;
    messageForm: { note: string };
    messageHandler: (event: ReactChangeEvent) => void;
    messageMultiline: boolean;
    background?: React.CSSProperties;
}

interface UserNoteProps {
    id: string;
    note: string;
    createdBy: string;
    createdAt: string;
    modifiedBy?: string;
    modifiedAt?: string;
}

interface RenderRowProps {
    id: string;
    note: string;
    index: number;
    selection?: string;
    onClick: (index: number) => void | undefined;
}

interface RenderTextProps extends UserNotesViewProps {
    value: string;
}

const RenderRow = (props: RenderRowProps): React.ReactElement => {
    const highlightClass = props.selection === props.id ? "user-notes-highlight-row" : "";
    return (
        <ListItem button className={highlightClass} key={props.id}>
            <ListItemText primary={props.note} onClick={() => props.onClick(props.index)} />
        </ListItem>
    );
};

const RenderText = (props: RenderTextProps): React.ReactElement => {
    return props.isLoading ? <Skeleton variant="text" /> : <>{props.value}</>;
};

export const UserNotesView = (props: UserNotesViewProps): React.ReactElement => {
    const hasNotes = props.userNotes && props.userNotes.length > 0;
    const noteUid = props.selection ? ` (${props.selection.id.substring(0, 8)}):` : ":";
    const isEmpty = Validate.isEmpty(props.messageForm.note);

    return (
        <section>
            <Backdrop className="backdrop" open={props.hasProgress}>
                <CircularProgress color="inherit" />
            </Backdrop>
            <Container className="container-wide">
                <div className="pt-120 pb-40">
                    <Card elevation={0} className="card">
                        <CardContent className="card-content">
                            <Typography component="span" className="caption black">
                                <RenderText {...props} value={props.captionText} />
                            </Typography>

                            <CustomDivider mt={15} mb={8} />

                            <div className="pt-25 pb-8">
                                <div className="mb-25">
                                    <Typography component="span" className="label">
                                        <RenderText {...props} value={props.descriptionText} />
                                    </Typography>
                                </div>

                                <div className="user-notes-box">
                                    <div className="user-notes-container">
                                        <Typography component="span" className="label">
                                            <RenderText {...props} value={`${props.listLabel}:`} />
                                        </Typography>
                                        <div
                                            className={`user-notes-fixed-list user-notes-border mt-10 ${props.isLoading ? "loading-indicator" : ""}`}
                                        >
                                            {!props.isLoading &&
                                                props.userNotes?.map((value: UserNoteProps, index: number) => (
                                                    <RenderRow
                                                        key={value.id}
                                                        id={value.id}
                                                        note={value.id.substring(0, 8)}
                                                        index={index}
                                                        selection={props.selection?.id}
                                                        onClick={props.onRowClick}
                                                    />
                                                ))}
                                        </div>
                                    </div>

                                    <div className="user-notes-message-box">
                                        <Typography component="span" className="label">
                                            <RenderText {...props} value={`${props.noteLabel}${noteUid}`} />
                                        </Typography>
                                        <div className="user-notes-text-box mt-10">
                                            <TextField
                                                required
                                                fullWidth
                                                multiline={props.messageMultiline}
                                                minRows={19}
                                                maxRows={19}
                                                id="note"
                                                name="note"
                                                variant="outlined"
                                                onChange={props.messageHandler}
                                                value={props.messageForm.note}
                                                disabled={props.isLoading}
                                            />
                                        </div>
                                    </div>
                                </div>

                                <div className="user-notes-button-box">
                                    <Button
                                        fullWidth
                                        type="submit"
                                        variant="contained"
                                        className="button-delete button-margin-right"
                                        disabled={props.isLoading || !hasNotes || !props.selection}
                                        onClick={props.removeButtonHandler}
                                    >
                                        {props.removeButtonText}
                                    </Button>
                                    <Button
                                        fullWidth
                                        type="submit"
                                        variant="contained"
                                        className="button-update button-margin-right"
                                        disabled={props.isLoading}
                                        onClick={props.clearButtonHandler}
                                    >
                                        {props.clearButtonText}
                                    </Button>
                                    <Button
                                        fullWidth
                                        type="submit"
                                        variant="contained"
                                        className="button-update"
                                        disabled={props.isLoading || isEmpty}
                                        onClick={props.saveButtonHandler}
                                    >
                                        {props.saveButtonText}
                                    </Button>
                                </div>
                            </div>
                        </CardContent>
                    </Card>
                </div>
            </Container>
        </section>
    );
};
