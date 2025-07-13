import * as React from "react";
import { ViewProperties } from "../../../../Shared/Abstractions";
import { CustomCard, Icon } from "../../../../Shared/Components";
import { TColour } from "../../../../Shared/types";
import { ExtendedViewProps } from "../accountActivate";

interface AccountActivateViewProps extends ViewProperties, ExtendedViewProps {
    shouldFallback: boolean;
    caption: string;
    text1: string;
    text2: string;
    fallback: {
        caption: string;
        text: string[];
    };
    hasProgress: boolean;
    hasError: boolean;
    hasSuccess: boolean;
}

const ProblemIcon = <Icon name="Alert" size={3} />;
const AlertIcon = <Icon name="AlertCircle" size={3} />;
const CheckIcon = <Icon name="Check" size={3} />;
const InfoIcon = <Icon name="Information" size={3} />;

export const AccountActivateView = (props: AccountActivateViewProps): React.ReactElement => {
    let icon = AlertIcon;
    let colour: TColour = "has-text-danger";
    if (props.hasSuccess) {
        icon = CheckIcon;
        colour = "has-text-success";
    } else {
        icon = InfoIcon;
        colour = "has-text-info";
    }

    return (
        <section className={props.className}>
            <div className="bulma-container bulma-is-max-desktop">
                <div className="py-6">
                    {props.shouldFallback ? (
                        <CustomCard
                            isLoading={props.isLoading}
                            caption={props.fallback?.caption}
                            text={props.fallback?.text}
                            icon={ProblemIcon}
                            colour="has-text-warning"
                        />
                    ) : (
                        <CustomCard
                            isLoading={props.isLoading}
                            caption={props.caption}
                            text={[props.text1, props.text2]}
                            icon={icon}
                            colour={colour}
                        />
                    )}
                </div>
            </div>
        </section>
    );
};
