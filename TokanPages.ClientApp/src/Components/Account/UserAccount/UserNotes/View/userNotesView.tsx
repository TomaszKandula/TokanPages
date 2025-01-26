import * as React from "react";
import { Skeleton } from "@material-ui/lab";
import { CustomDivider } from "../../../../../Shared/Components";
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
    Typography 
} from "@material-ui/core";

interface UserNotesViewProps {
    isLoading: boolean;
    hasProgress: boolean;
    userNotes?: UserNotesProps[] | undefined;
    captionText: string;
    descriptionText: string;
    onRowClick: (id: string) => void;
    removeButtonText: string;
    removeButtonHandler: () => void;
    saveButtonText: string;
    saveButtonHandler: () => void;
    messageValue: string;
    messageHandler: () => void;
    messageMultiline: boolean;
    background?: React.CSSProperties;
}

interface RenderRowProps {
    index: string;
    note: string;
    style?: React.CSSProperties | undefined;
    onClick: (id: string) => void | undefined;
}

interface RenderTextProps extends UserNotesViewProps {
    value: string;
}

interface UserNotesProps {
    id: string;
    note: string;
    createdBy: string;
    createdAt: string;
    modifiedBy: string;
    modifiedAt: string;
}

const RenderRow = (props: RenderRowProps): React.ReactElement => {
    return (
        <ListItem button style={props.style} key={props.index}>
            <ListItemText primary={props.note} onClick={() => props.onClick(props.index) } />
        </ListItem>
    );
}

const RenderText = (props: RenderTextProps): React.ReactElement => {
    return props.isLoading ? <Skeleton variant="text" /> : <>{props.value}</>;
};

export const UserNotesView = (props: UserNotesViewProps): React.ReactElement => {
    const hasNotes = props.userNotes && props.userNotes.length > 0;
    return(
        <section>
            <Backdrop className="backdrop" open={props.hasProgress}>
                <CircularProgress color="inherit" />
            </Backdrop>
            <Container className="container-wide">
                <div style={{ paddingTop: 120, paddingBottom: 40 }}>
                    <Card elevation={0} className="card">
                        <CardContent className="card-content">
                            <div style={{ paddingTop: 0, paddingBottom: 0 }}>
                                <Typography component="span" className="caption black">
                                    <RenderText {...props} value={props.captionText} />
                                </Typography>
                            </div>

                            <CustomDivider marginTop={16} marginBottom={8} />

                            <div style={{ paddingTop: 24, paddingBottom: 8 }}>
                                <div style={{ marginBottom: 24 }}>
                                    <Typography component="span" className="label">
                                        <RenderText {...props} value={props.descriptionText} />
                                    </Typography>
                                </div>

                                <div className="user-notes-box">
                                    <div className={`user-notes-fixed-list ${props.isLoading ? "loading-indicator" : ""}`}>
                                        {!props.isLoading && props.userNotes?.map((value: UserNotesProps) => (
                                            <RenderRow 
                                                key={value.id} 
                                                index={value.id} 
                                                note={value.note} 
                                                onClick={props.onRowClick} 
                                            />
                                        ))}
                                    </div>

                                    <div className="user-notes-message-box">
                                        <div className={"user-notes-text-box"}>
                                            <TextField
                                                required
                                                fullWidth
                                                multiline={props.messageMultiline}
                                                minRows={12}
                                                id="message"
                                                name="message"
                                                autoComplete="message"
                                                variant="outlined"
                                                onChange={props.messageHandler}
                                                value={props.messageValue}
                                                disabled={props.isLoading}
                                            />
                                        </div>

                                        <div className="user-notes-button-box">
                                            <Button
                                                fullWidth
                                                type="submit"
                                                variant="contained"
                                                className="button-delete button-delete-margin-right"
                                                disabled={props.isLoading || !hasNotes}
                                                onClick={props.removeButtonHandler}
                                            >
                                                {props.removeButtonText}
                                            </Button>
                                            <Button
                                                fullWidth
                                                type="submit"
                                                variant="contained"
                                                className="button-update"
                                                disabled={props.isLoading}
                                                onClick={props.saveButtonHandler}
                                            >
                                                {props.saveButtonText}
                                            </Button>
                                        </div>

                                    </div>

                                </div>
                            </div>
                        </CardContent>
                    </Card>
                </div>
            </Container>
        </section>
    );
}
