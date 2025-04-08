import * as React from "react";
import Container from "@material-ui/core/Container";
import ReportProblemIcon from "@material-ui/icons/ReportProblem";
import InfoIcon from "@material-ui/icons/Info";
import ErrorIcon from "@material-ui/icons/Error";
import DoneIcon from "@material-ui/icons/Done";
import { ViewProperties } from "../../../../Shared/Abstractions";
import { CustomCard } from "../../../../Shared/Components";
import { ExtendedViewProps } from "../accountActivate";

interface AccountActivateViewProps extends ViewProperties, ExtendedViewProps {
    shouldFallback: boolean;
    caption: string;
    text1: string;
    text2: string;
    hasProgress: boolean;
    hasError: boolean;
    hasSuccess: boolean;
}

export const AccountActivateView = (props: AccountActivateViewProps): React.ReactElement => {
    return (
        <section className={`section ${props.background ?? ""}`}>
            <Container className="container-wide">
                <div className={!props.className ? "pt-0 pb-15" : props.className}>
                    {props.shouldFallback ? 
                    <CustomCard  
                        isLoading={props.isLoading}
                        caption="aaaaaaaaaaaaaaaaa"//TODO: add content
                        text={["bbbbbbbbbbbbbbbb"]}//TODO: add content
                        icon={<ReportProblemIcon />}
                        colour="warning"
                    /> 
                    : <CustomCard  
                        isLoading={props.isLoading}
                        caption={props.caption}
                        text={[props.text1, props.text2]}
                        icon={props.hasError ? <ErrorIcon /> : props.hasSuccess ? <DoneIcon /> : <InfoIcon />}
                        colour={props.hasError ? "error" : props.hasSuccess ? "success" : "info"}
                    />}
                </div>
            </Container>
        </section>
    );
};
