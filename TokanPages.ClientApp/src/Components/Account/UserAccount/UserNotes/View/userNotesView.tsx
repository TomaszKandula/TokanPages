import * as React from "react";
import { Button, Card, CardContent, Container, ListItem, ListItemText, TextField, Typography } from "@material-ui/core";
import { CustomDivider } from "../../../../../Shared/Components";

interface RenderRowProps {
    index: string;
    note: string;
    style?: React.CSSProperties | undefined;
    onClick: (id: string) => void | undefined;
}

const RenderRow = (props: RenderRowProps): React.ReactElement => {
    return (
        <ListItem button style={props.style} key={props.index}>
            <ListItemText primary={props.note} onClick={() => props.onClick(props.index) } />
        </ListItem>
    );
}

interface UserNotesProps {
    id: string;
    note: string;
}

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
}

export const UserNotesView = (props: UserNotesViewProps): React.ReactElement => {
    return(
        <section>
            <Container className="container-wide">
                <div style={{ paddingTop: 120, paddingBottom: 40 }}>
                    <Card elevation={0} className="card">
                        <CardContent className="card-content">
                            <div style={{ paddingTop: 0, paddingBottom: 0 }}>
                                <Typography component="span" className="caption black">
                                    {props.captionText}
                                </Typography>
                            </div>

                            <CustomDivider marginTop={16} marginBottom={8} />

                            <div style={{ paddingTop: 24, paddingBottom: 8 }}>
                                <div style={{ marginBottom: 24 }}>
                                    <Typography component="span" className="label">
                                        {props.descriptionText}
                                    </Typography>
                                </div>

                                <div className="user-notes-box">
                                    <div className="user-notes-fixed-list">
                                        {props.userNotes?.map((value: UserNotesProps) => (
                                            <RenderRow 
                                                key={value.id} 
                                                index={value.id} 
                                                note={value.note} 
                                                onClick={props.onRowClick} 
                                            />
                                        ))}
                                    </div>

                                    <div className="user-notes-message-box">
                                        <div className="user-notes-text-box">
                                            <TextField
                                                required
                                                fullWidth
                                                multiline={true}
                                                minRows={12}
                                                id="message"
                                                name="message"
                                                autoComplete="message"
                                                variant="outlined"
                                                onChange={props.messageHandler}
                                                value={props.messageValue}
                                            />
                                        </div>

                                        <div className="user-notes-button-box">
                                            <Button
                                                fullWidth
                                                type="submit"
                                                variant="contained"
                                                className="button-delete button-delete-margin-right"
                                                onClick={props.removeButtonHandler}
                                            >
                                                {props.removeButtonText}
                                            </Button>
                                            <Button
                                                fullWidth
                                                type="submit"
                                                variant="contained"
                                                className="button-update"
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
