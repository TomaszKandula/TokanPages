import * as React from "react";
import { FixedSizeList, ListChildComponentProps } from "react-window";
import { Card, CardContent, Container, ListItem, ListItemText, Typography } from "@material-ui/core";
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

                            <FixedSizeList height={400} width={300} itemSize={46} itemCount={20}>
                                {renderRow}
                            </FixedSizeList>

                        </CardContent>
                    </Card>
                </div>
            </Container>
        </section>
    );
}
