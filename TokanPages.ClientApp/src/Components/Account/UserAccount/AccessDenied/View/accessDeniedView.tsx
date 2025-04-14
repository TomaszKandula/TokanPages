import * as React from "react";
import Container from "@material-ui/core/Container";
import CancelIcon from "@material-ui/icons/Cancel";
import { CustomCard } from "../../../../../Shared/Components";
import { AccessDeniedProps } from "../accessDenied";

interface AccessDeniedViewProps extends AccessDeniedProps {
    isLoading: boolean;
    languageId: string;
    accessDeniedCaption: string;
    accessDeniedPrompt: string[];
    homeButtonText: string;
}

export const AccessDeniedView = (props: AccessDeniedViewProps): React.ReactElement => {
    return (
        <section className={`section ${props.background ?? ""}`}>
            <Container className="container-wide">
                <div className="pt-120 pb-64">
                    <CustomCard
                        isLoading={props.isLoading}
                        caption={props.accessDeniedCaption}
                        text={props.accessDeniedPrompt}
                        icon={<CancelIcon />}
                        colour="error"
                    />
                </div>
            </Container>
        </section>
    );
};
