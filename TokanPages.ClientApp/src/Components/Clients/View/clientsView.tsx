import * as React from "react";
import { useSelector } from "react-redux";
import Skeleton from "@material-ui/lab/Skeleton";
import { Box, Container, Typography } from "@material-ui/core";
import { ApplicationState } from "../../../Store/Configuration";
import { ContentClientsState } from "../../../Store/States";
import { GET_ICONS_URL } from "../../../Api/Request";
import { ClientsStyle } from "./clientsStyle";
import { v4 as uuidv4 } from "uuid";
import Validate from "validate.js";

const RenderCaption = (props: ContentClientsState): JSX.Element | null => {
    const classes = ClientsStyle();
    if (!Validate.isEmpty(props?.content?.caption)) {
        return (
            <Box mb={8}>
                <Typography className={classes.caption}>{props?.content?.caption?.toUpperCase()}</Typography>
            </Box>
        );
    }

    return null;
};

const RenderImages = (props: ContentClientsState): JSX.Element => {
    const classes = ClientsStyle();
    const getImagePath = (value: string): string => `${GET_ICONS_URL}/${value}`;
    return (
        <Box pt={4} display="flex" flexWrap="wrap" justifyContent="center">
            {props?.content?.images.map((item: string, _index: number) => (
                <img key={uuidv4()} src={getImagePath(item)} alt="" className={classes.logo} />
            ))}
        </Box>
    );
};

export const ClientsView = (): JSX.Element => {
    const classes = ClientsStyle();
    const clients = useSelector((state: ApplicationState) => state.contentClients);

    return (
        <>
            <section className={classes.section}>
                <Container maxWidth="lg">
                    {clients?.isLoading ? <Skeleton variant="text" /> : <RenderCaption {...clients} />}
                    {clients?.isLoading ? <Skeleton variant="rect" height="48px" /> : <RenderImages {...clients} />}
                </Container>
            </section>
        </>
    );
};
