import * as React from "react";
import Skeleton from "@material-ui/lab/Skeleton";
import { Box, Container, Typography } from "@material-ui/core";
import { IGetClientsContent } from "../../Redux/States/Content/getClientsContentState";
import { ICONS_PATH } from "../../Shared/constants";
import ClientsStyle from "./clientsStyle";
import Validate from "validate.js";

const ClientsView = (props: IGetClientsContent): JSX.Element => 
{
    const classes = ClientsStyle();

    const getImagePath = (value: string): string => 
    {
        return `${ICONS_PATH}${value}`;
    }

    const RenderCaption = (): JSX.Element | null => 
    {
        if (!Validate.isEmpty(props.content?.caption))
        {
            return(
                <Typography className={classes.caption}>
                    {props.content?.caption?.toUpperCase()}                    
                </Typography>
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
                    <Box mb={8}>
                        {props.isLoading ? <Skeleton variant="text" /> : <RenderCaption />}
                    </Box>
                    {props.isLoading ? <Skeleton variant="rect" height="48px" /> : <RenderImages />}
                </Container>
            </section>
        </>
    );
}

export default ClientsView;
