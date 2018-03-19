import * as React from 'react';
import * as ReactDOM from 'react-dom';
import * as Remarkable from 'remarkable';

export interface ICommentBox {
    model: Array<IComment>;
}

export interface ICommentList {
    list: Array<IComment>;
}

export interface IComment {
    id: number;
    author: string;
    text: string;
}

namespace App {
    const htmlView = document.querySelector('.view');
    const model: Array<IComment> = [
        { id: 1, author: "Daniel Lo Nigro del Johnny", text: "Hello ReactJS.NET World!" },
        { id: 2, author: "Pete Hunt", text: "This is one comment" },
        { id: 3, author: "Jordan Walke", text: "This is *another* comment" }
    ];

    class CommentBox extends React.Component<ICommentBox> {
        render() {

            return (
                <div className="commentBox">
                    <h1>Comments</h1>
                    <CommentList list={this.props.model} />
                    <CommentForm />
                </div>
            );
        }
    }

    class CommentList extends React.Component<ICommentList> {
        render() {
            return (
                <div className="commentList">
                    {this.props.list.map(comment => <Comment key={comment.id} {...comment} />)}
                </div>
            );
        }
    }

    class CommentForm extends React.Component {
        render() {
            return (
                <div className="commentForm">
                    Hello, world! I am a CommentForm.
                </div>
            );
        }
    }

    class Comment extends React.Component<IComment> {
        render() {
            return (
                <div className="comment">
                    <h2 className="commentAuthor">
                        {this.props.author}
                    </h2>
                    <div dangerouslySetInnerHTML={{ __html: this.props.text }}></div>
                </div>
            );
        }
    }

    ReactDOM.render(<CommentBox model={model} />, htmlView);
}
