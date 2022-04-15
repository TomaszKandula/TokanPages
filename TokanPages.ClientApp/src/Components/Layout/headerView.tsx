import * as React from "react";
import { Link } from "react-router-dom";
import Container from "@material-ui/core/Container";
import Box from "@material-ui/core/Box";
import Typography from "@material-ui/core/Typography";
import Skeleton from "@material-ui/lab/Skeleton";
import Button from "@material-ui/core/Button";
import Grid from "@material-ui/core/Grid/Grid";
import { IGetHeaderContent } from "../../Redux/States/Content/getHeaderContentState";
import { IMAGES_PATH } from "../../Shared/constants";
import headerStyle from "./Styles/headerStyle";
import { renderImage } from "../../Shared/Components/CustomImage/customImage";

const HeaderView = (props: IGetHeaderContent): JSX.Element => 
{
    const classes = headerStyle();

    const ActiveButton = (): JSX.Element => 
    {
        return(
            <Link to="/mystory" className={classes.action_link}>
                <Button variant="contained" className={classes.action_button}>{props.content?.action}</Button>
            </Link>
        );
    }

    const RenderImage = (): JSX.Element => 
    {
        return(
            <div data-aos="fade-right">
                {renderImage(IMAGES_PATH, props.content?.photo, classes.image)}
            </div>
        );
    }

    return (
        <section>
            <Container maxWidth="lg">
                <Grid container className={classes.top_margin}>
                    <Grid item xs={12} sm={6}>
                        <Box className={classes.image_box}>
                            {props.isLoading ? <Skeleton variant="circle" className={classes.image_skeleton} /> : <RenderImage />}
                        </Box>
                    </Grid>
                    <Grid item xs={12} sm={6}>
                        <Box className={classes.content_box}>
                            <div data-aos="fade-left">
                                <Typography variant="h3" gutterBottom={true}>
                                    {props.isLoading ? <Skeleton variant="text" /> : props.content?.caption}
                                </Typography>
                                <Typography variant="h6" className={classes.content_description}>
                                    {props.isLoading ? <Skeleton variant="text" /> : props.content?.description}
                                </Typography>
                                <Box mt={4}>
                                    {props.isLoading ? <Skeleton variant="rect" height="48px" /> : <ActiveButton />}
                                </Box>
                            </div>
                        </Box>
                    </Grid>
                </Grid>
            </Container>
        </section>
	);
}

export default HeaderView;
