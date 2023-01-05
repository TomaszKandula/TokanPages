import * as React from "react";
import Skeleton from "@material-ui/lab/Skeleton";
import { Box, Container, Typography } from "@material-ui/core";
import { IContentClients } from "../../../Store/States";
import { GET_ICONS_URL } from "../../../Api/Request";
import { ClientsStyle } from "./clientsStyle";
import Validate from "validate.js";

export const ClientsView = (props: IContentClients): JSX.Element => 
{
    const classes = ClientsStyle();

    const getImagePath = (value: string): string => 
    {
        return `${GET_ICONS_URL}/${value}`;
    }

    const RenderCaption = (): JSX.Element | null => 
    {
        if (!Validate.isEmpty(props.content?.caption))
        {
            return(
                <Box mb={8}>
                    <Typography className={classes.caption}>
                        {props.content?.caption?.toUpperCase()}                    
                    </Typography>
                </Box>
            );
        }

        return null;
    }

    const RenderImages = (): JSX.Element => 
    {
        return(
            <Box pt={4} display="flex" flexWrap="wrap" justifyContent="center">
                {props.content?.images.map((item, index) => (
                    <img key={index} src={getImagePath(item)} alt="" className={classes.logo} />
                ))}
            </Box>
        );
    }

    return(
        <>
            <div className={classes.divider}></div>
            <section className={classes.section}>
                <Container maxWidth="lg">
                    {props.isLoading ? <Skeleton variant="text" /> : <RenderCaption />}
                    {props.isLoading ? <Skeleton variant="rect" height="48px" /> : <RenderImages />}
                </Container>
            </section>
        </>
    );
}
