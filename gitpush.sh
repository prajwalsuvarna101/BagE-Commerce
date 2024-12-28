#!/bin/bash

# Check if a commit message argument is provided
if [ -z "$1" ]; then
    echo "Error: Please provide a commit message."
    echo "Usage: ./git_commit_push.sh \"Your commit message\""
    exit 1
fi

# Stage all changes
git add .

# Commit the changes with the provided message
git commit -m "$1"

# Push the changes to the remote repository
git push

# Notify user of success
echo "Changes committed and pushed successfully with message: '$1'"
