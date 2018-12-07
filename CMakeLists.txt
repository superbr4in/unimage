cmake_minimum_required ( VERSION 3.8 )

set ( CMAKE_CXX_STANDARD 17 )

add_library ( unimage SHARED
    "source/utf8_base.cpp"
    "source/utf8_canvas.cpp"
    "source/utf8_shape.cpp" )

target_include_directories ( unimage PUBLIC
    "include/" )
