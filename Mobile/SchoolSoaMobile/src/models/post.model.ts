export class Post {
    id: string;
    blogId: string;
    creatorId: string;

    content: string;
}

export class InsertPost {
  blogId: string;
  content: string;
}
