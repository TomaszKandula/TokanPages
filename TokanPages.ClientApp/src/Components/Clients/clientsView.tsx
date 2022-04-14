import { Box, Container } from "@material-ui/core";
import Skeleton from "@material-ui/lab/Skeleton";
import * as React from "react";
import { IGetClientsContent } from "../../Redux/States/Content/getClientsContentState";
import { ICONS_PATH } from "../../Shared/constants";
import ClientsStyle from "./clientsStyle";

const ClientsView = (props: IGetClientsContent): JSX.Element => 
{
    const classes = ClientsStyle();

    const getImagePath = (value: string): string => 
    {
        return `${ICONS_PATH}${value}`;
    }

    const RenderImages = (): JSX.Element => 
    {
        return(
            <Box pt={4} display="flex" flexWrap="wrap" justifyContent="center">
                <div data-aos="zoom-in">
                    {props.content?.images.map((item, index) => (
                        <img key={index} src={getImagePath(item)} alt="" className={classes.logo} />
                    ))}
                </div>
            </Box>
        );
    }

    return(
        <>
        <div className={classes.divider}></div>
        <section className={classes.section}>
            <Container maxWidth="lg">
                {props.isLoading ? <Skeleton variant="rect" height="48px" /> : <RenderImages />}
            </Container>
        </section>
        </>
    );
}

export default ClientsView;
