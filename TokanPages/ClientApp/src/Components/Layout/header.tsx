import * as React from "react";
import { Link } from "react-router-dom";
import Container from "@material-ui/core/Container";
import Box from "@material-ui/core/Box";
import Typography from "@material-ui/core/Typography";
import Button from "@material-ui/core/Button";
import Grid from "@material-ui/core/Grid/Grid";
import { IMAGES_PATH } from "../../Shared/constants";
import { IHeaderContentDto } from "../../Api/Models";
import useStyles from "./Hooks/styleHeader";
import { renderImage } from "../../Shared/Components/CustomImage/customImage";

export default function Header(props: { header: IHeaderContentDto, isLoading: boolean }) 
{
    const classes = useStyles();
    return (
        <section>
            <Container maxWidth="lg">
                <Grid container className={classes.gridMargin}>
                    <Grid item xs={12} sm={6}>
                        <Box className={classes.imageBox}>
                            <div data-aos="fade-right">
                                {renderImage(IMAGES_PATH, props.header.content.photo, classes.img)}
                            </div>
                        </Box>
                    </Grid>
                    <Grid item xs={12} sm={6}>
                        <Box className={classes.contentBox}>
                            <div data-aos="fade-left">
                                <Typography variant="overline" component="span" gutterBottom={true}>
                                    {props.header.content.caption}
                                </Typography>
                                <Typography variant="h5" color="textSecondary" paragraph={true}>
                                    {props.header.content.description}
                                </Typography>
                                <Box mt={4}>
                                    <Link to="/mystory" className={classes.mainLink}>
                                        <Button variant="contained" className={classes.mainAction}>{props.header.content.action}</Button>
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
