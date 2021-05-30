import * as React from "react";
import { Link } from "react-router-dom";
import Container from "@material-ui/core/Container";
import Box from "@material-ui/core/Box";
import Typography from "@material-ui/core/Typography";
import Button from "@material-ui/core/Button";
import Grid from "@material-ui/core/Grid/Grid";
import { IGetHeaderContent } from "../../Redux/States/getHeaderContentState";
import { IMAGES_PATH } from "../../Shared/constants";
import headerStyle from "./Styles/headerStyle";
import { renderImage } from "../../Shared/Components/CustomImage/customImage";

export default function HeaderView(props: IGetHeaderContent) 
{
    const classes = headerStyle();
    return (
        <section>
            <Container maxWidth="lg">
                <Grid container className={classes.gridMargin}>
                    <Grid item xs={12} sm={6}>
                        <Box className={classes.imageBox}>
                            <div data-aos="fade-right">
                                {renderImage(IMAGES_PATH, props.content?.photo, classes.img)}
                            </div>
                        </Box>
                    </Grid>
                    <Grid item xs={12} sm={6}>
                        <Box className={classes.contentBox}>
                            <div data-aos="fade-left">
                                <Typography variant="overline" component="span" gutterBottom={true}>
                                    {props.content?.caption}
                                </Typography>
                                <Typography variant="h5" color="textSecondary" paragraph={true}>
                                    {props.content?.description}
                                </Typography>
                                <Box mt={4}>
                                    <Link to="/mystory" className={classes.mainLink}>
                                        <Button variant="contained" className={classes.mainAction}>{props.content?.action}</Button>
                                    </Link>
                                </Box>
                            </div>
                        </Box>
                    </Grid>
                </Grid>
            </Container>
        </section>
	);
}
