import * as React from "react";
import { useSelector } from "react-redux";
import Skeleton from "@material-ui/lab/Skeleton";
import { Box, Container, Typography } from "@material-ui/core";
import { ApplicationState } from "../../../Store/Configuration";
import { GET_ICONS_URL } from "../../../Api/Request";
import { ClientsContentDto } from "../../../Api/Models";
import { ClientsStyle } from "./clientsStyle";
import { v4 as uuidv4 } from "uuid";
import Validate from "validate.js";

interface ClientsViewProps {
    background?: React.CSSProperties;
}

const RenderCaption = (props: ClientsContentDto): React.ReactElement | null => {
    const classes = ClientsStyle();
    if (!Validate.isEmpty(props?.caption)) {
        return (
            <Box mb={8}>
                <Typography className={classes.caption}>{props?.caption?.toUpperCase()}</Typography>
            </Box>
        );
    }

    return null;
};

const RenderImages = (props: ClientsContentDto): React.ReactElement => {
    const classes = ClientsStyle();
    const getImagePath = (value: string): string => `${GET_ICONS_URL}/${value}`;
    return (
        <Box pt={4} display="flex" flexWrap="wrap" justifyContent="center">
            {props?.images.map((item: string, _index: number) => (
                <img key={uuidv4()} src={getImagePath(item)} alt={`image of ${item}`} className={classes.logo} />
            ))}
        </Box>
    );
};

export const ClientsView = (props: ClientsViewProps): React.ReactElement => {
    const classes = ClientsStyle();
    const data = useSelector((state: ApplicationState) => state.contentPageData);
    const clients = data.components.clients;

    return (
        <>
            <section className={classes.section} style={props.background}>
                <Container maxWidth="lg">
                    {data?.isLoading ? <Skeleton variant="text" /> : <RenderCaption {...clients} />}
                    {data?.isLoading ? <Skeleton variant="rect" height="48px" /> : <RenderImages {...clients} />}
                </Container>
            </section>
        </>
    );
};
