import * as React from "react";
import { Link } from "react-router-dom";
import Container from "@material-ui/core/Container";
import Box from "@material-ui/core/Box";
import Typography from "@material-ui/core/Typography";
import { Divider, IconButton } from "@material-ui/core";
import { ArrowBack } from "@material-ui/icons";
import ReactHtmlParser from "react-html-parser";
import axios from "axios";
import useStyles from "./styleStaticContent";
import Validate from "validate.js";
import CenteredCircularLoader from "Shared/ProgressBar/centeredCircularLoader";

interface IStoryContent
{
    dataUrl: string;
}

export default function StaticContent(props: IStoryContent) 
{

    const classes = useStyles();
    const [ data, setData ] = React.useState("");

    const fetchData = React.useCallback( async () => 
    {
        const response = await axios.get(props.dataUrl, {method: "get", responseType: "text"});
        setData(response.data);
    }, 
    [ props.dataUrl ]);

    React.useEffect( () => { fetchData() }, [ data, fetchData ] );

    const renderData = (text: string) => 
    {
        return(
            <div data-aos="fade-up">
                <Typography variant="body1" component="span" className={classes.typography}>
                    {ReactHtmlParser(text)}
                </Typography>
            </div>
        );
    }

    return (
        <section>
            <Container className={classes.container}>       
                <Box py={12}>
                    <Link to="/">
                        <IconButton>
                            <ArrowBack/>
                        </IconButton>
                    </Link> 
                    <Divider className={classes.divider} />
                    {Validate.isEmpty(data) ? <CenteredCircularLoader /> : renderData(data)}
                </Box>
            </Container>
        </section>
    );

}
