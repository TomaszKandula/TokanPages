import * as React from "react";
import { Link } from "react-router-dom";
import Container from "@material-ui/core/Container";
import Box from "@material-ui/core/Box";
import { Divider, IconButton } from "@material-ui/core";
import { ArrowBack } from "@material-ui/icons";
import { ProgressBar } from "../../../Shared/Components/";
import { RenderContent } from "../../../Shared/Components";
import { ITextObject } from "../../../Shared/Components/RenderContent/Models";
import { StaticContentStyle } from "./staticContentStyle";

interface IBinding
{
    bind: IProperties;
}

interface IProperties
{
    data: ITextObject;
    isLoading: boolean;
}

export const StaticContentView = (props: IBinding): JSX.Element => 
{
    const classes = StaticContentStyle();
    return (
        <section className={classes.section}>
            <Container className={classes.container}>
                <Box py={12}>
                    <div data-aos="fade-down">
                        <Link to="/">
                            <IconButton>
                                <ArrowBack/>
                            </IconButton>
                        </Link> 
                        <Divider className={classes.divider} />
                    </div>                    
                    <div data-aos="fade-up">
                        {props.bind?.isLoading 
                            ? <ProgressBar /> 
                            : <RenderContent items={props.bind?.data.items}/>}
                    </div>
                </Box>
            </Container>
        </section>
    );
}
