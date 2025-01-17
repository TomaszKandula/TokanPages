import * as React from "react";
import { FixedSizeList, ListChildComponentProps } from "react-window";
import { Button, Card, CardContent, Container, ListItem, ListItemText, TextField, Typography } from "@material-ui/core";
import { CustomDivider } from "../../../../../Shared/Components";

const renderRow = (props: ListChildComponentProps) => {
    return (
        <ListItem button style={props.style} key={props.index}>
            <ListItemText primary={`Item ${props.index + 1}`} />
        </ListItem>
    );
}

export const UserNotesView = (): React.ReactElement => {

    return(
        <section>
            <Container className="container-wide">
                <div style={{ paddingTop: 120, paddingBottom: 40 }}>
                    <Card elevation={0} className="card">
                        <CardContent className="card-content">
                            <div style={{ paddingTop: 0, paddingBottom: 0 }}>
                                <Typography component="span" className="caption black">
                                    User Notes
                                </Typography>
                            </div>

                            <CustomDivider marginTop={16} marginBottom={8} />

                            <div style={{ paddingTop: 24, paddingBottom: 8 }}>
                                <div style={{ marginBottom: 24 }}>
                                    <Typography component="span" className="label">
                                        Some text
                                    </Typography>
                                </div>

                                <div className="user-notes-box">
                                    <FixedSizeList 
                                        height={400}
                                        width={"60%"}
                                        itemSize={46}
                                        itemCount={20}
                                        className="user-notes-fixed-list"
                                    >
                                        {renderRow}
                                    </FixedSizeList>

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
                                                //onChange={props.formHandler}
                                                //value={props.message}
                                                //label={props.labelMessage}
                                            />
                                        </div>

                                        <div className="user-notes-button-box">
                                            <Button
                                                fullWidth
                                                type="submit"
                                                variant="contained"
                                                className="button-delete button-delete-margin-right"
                                            >
                                                Remove
                                            </Button>
                                            <Button
                                                fullWidth
                                                type="submit"
                                                variant="contained"
                                                className="button-update"
                                            >
                                                Save
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
