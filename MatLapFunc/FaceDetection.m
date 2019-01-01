function [ Output ] = FaceDetection( img )
% Face Detection step....
%
% Description: This function detects the face in image
%
% Argument:      img                             -Input image
% 
% Returns:      Output                           -Face image detected.
face = vision.CascadeObjectDetector('MergeThreshold',10);
BB=step(face,img);
Output = imcrop(img,BB);
end

