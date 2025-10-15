import * as React from "react";
import { useDimensions, useErrorBoundaryContent } from "../../../Shared/Hooks";
import { Card } from "../Card";
import { Icon } from "../Icon";

export const ErrorBoundaryView = () => {
    const media = useDimensions();
    const content = useErrorBoundaryContent();

    return (
        <div className="bulma-container is-flex is-justify-content-center is-align-self-center">
            <div className="mt-6 pt-6" style={{ minWidth: media.isDesktop ? "720px" : undefined }}>
                <Card
                    isLoading={false}
                    caption={content?.title}
                    text={[content?.subtitle, content?.text]}
                    icon={<Icon name="Alert" size={3} />}
                    colour="has-text-danger"
                    externalContent={
                        <div className="">
                            <hr />
                            <a href={content?.linkHref}>
                                <p>{content?.linkText}</p>
                            </a>
                            <p>{content?.footer}</p>
                        </div>
                    }
                />
            </div>
        </div>
    );
};
