function [ x ] = ValidateImage( imagePath )
image = imread(imagePath);
image = FaceDetection(image);
[R C] = size(image);
if(R == 0 && C == 0)
    x = 0 
else
    x = 1
end

end

